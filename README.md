# What's this?

Yet another JSON library, specificially for my F# JSON needs which were not met by JSON.Net or Chiron

## How it works (briefly)
**JSON text**

↕ parsed (using FParsec)

**JSON data structure (as a DU)**

↕ mapped (using codecs)

**Your F# types**

## Key design principles

- No care for how the outputted JSON text is formatted
- Null-safety: You can't deserialise JSON to get null for non-nullable F# objects etc
- Support for all simple F# types out-of-box
- Simple code: Exceptions are thrown internally instead of railroading/result types
- Easily extensible: You can add codecs for types as you need

## Getting started

You can get this package on [NuGet](https://www.nuget.org/packages/Percyqaz.Json/)

You should read [the documentation here](https://github.com/percyqaz/Percyqaz.Json/wiki) (it's not long)
