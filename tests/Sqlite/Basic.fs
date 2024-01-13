﻿namespace Percyqaz.Data.Tests.Sqlite

open System
open NUnit.Framework
open Percyqaz.Data.Sqlite

[<TestFixture>]
type Basic() =

    [<Test>] 
    member this.RoundTripById() =
        let user : User = { 
            Username = "Percyqaz"
            DateLastSeen = DateTimeOffset.Now.ToUnixTimeSeconds() |> Some
            Badges = Set.ofList [ "badge1"; "badge2" ]
            Score = Math.PI * 5.2
        }

        let db = Database.in_memory()
        db |> Database.create_table Users.TABLE |> expect |> printfn "Table created; %A rows modified"
        let user_id = db |> Users.INSERT.ExecuteGetId user |> expect

        let retrieved_user_by_id = db |> Users.QUERY_BY_ID.Execute user_id |> expect |> Seq.exactlyOne
        Assert.AreEqual(user, retrieved_user_by_id)
    
    [<Test>] 
    member this.RoundTripByQueryAll() =
        let user : User = { 
            Username = "Percyqaz"
            DateLastSeen = DateTimeOffset.Now.ToUnixTimeSeconds() |> Some
            Badges = Set.ofList [ "badge1"; "badge2" ]
            Score = Math.PI * 5.2
        }

        let db = Database.in_memory()
        db |> Database.create_table Users.TABLE |> expect |> printfn "Table created; %A rows modified"
        let user_id = db |> Users.INSERT.ExecuteGetId user |> expect

        let retrieved_id, retrieved_user = db |> Users.QUERY_ALL.Execute () |> expect |> Seq.exactlyOne
        Assert.AreEqual(user_id, retrieved_id)
        Assert.AreEqual(user, retrieved_user)
        
    [<Test>] 
    member this.RoundTripBatchInsert() =
        let random = Random()
        let users = List.init 10000 (fun i ->
                { 
                    Username = sprintf "User_%i" i
                    DateLastSeen = DateTimeOffset.Now.ToUnixTimeSeconds() |> Some
                    Badges = Set.ofList [ "badge1"; "badge2"; "badge3" ]
                    Score = Math.PI * random.NextDouble()
                }
            )

        let db = Database.in_memory()
        db |> Database.create_table Users.TABLE |> expect |> printfn "Table created; %A rows modified"
        db |> Users.INSERT.Batch users |> expect |> printfn "Batch insert; %A rows modified"

        let retrieved_users = db |> Users.QUERY_ALL.Execute () |> expect |> Seq.sortBy fst |> Seq.map snd |> List.ofSeq
        Assert.AreEqual(10000, retrieved_users.Length)
        Assert.AreEqual(users, retrieved_users)
    
    [<Test>] 
    member this.RoundTripBatchInsertOnDisk() =
        let random = Random()
        let users = List.init 10000 (fun i ->
                { 
                    Username = sprintf "User_%i" i
                    DateLastSeen = DateTimeOffset.Now.ToUnixTimeSeconds() |> Some
                    Badges = Set.ofList [ "badge1"; "badge2"; "badge3" ]
                    Score = Math.PI * random.NextDouble()
                }
            )

        let db = Database.from_file("database.db")
        db |> Database.drop_table_if_exists Users.TABLE |> expect |> printfn "Table dropped if exists; %A rows modified"
        db |> Database.create_table Users.TABLE |> expect |> printfn "Table created; %A rows modified"
        db |> Users.INSERT.Batch users |> expect |> printfn "Batch insert; %A rows modified"

        let retrieved_users = db |> Users.QUERY_ALL.Execute () |> expect |> Seq.sortBy fst |> Seq.map snd |> List.ofSeq
        Assert.AreEqual(10000, retrieved_users.Length)
        Assert.AreEqual(users, retrieved_users)