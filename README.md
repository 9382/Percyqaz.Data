# Percyqaz.Json

Yet another JSON library, specificially for my F# JSON needs which were not met by JSON.Net or Chiron

## How it works:
1. **Parsing and writing:**
    - JSON text is parsed using FParsec into an F# discriminated union/tree.
    - The tree can be formatted back as valid JSON text.
2. **Encoding and decoding:**
    - Each type has a hand-made codec that maps between it and the JSON tree.
3. **Ease of use:** 
    - Many important codecs are ready out-of-the-box.
    - It is simple to hand-write your own codecs for types.
    - Records and DUs can be tagged with an attribute to auto-generate a codec.

## Key design principles

- No care for how the outputted JSON text is formatted (validity is the only concern)
- Null-safety: You can't deserialise JSON to get null for non-nullable F# objects etc
- Support for all simple F# types out-of-box
- Simple code: Exceptions are thrown internally instead of railroading/result types
- Easily extensible: You can add codecs for types as you need

## Getting started

You can get this package on [NuGet](https://www.nuget.org/packages/Percyqaz.Json/)

You should read [the documentation here](https://github.com/percyqaz/Percyqaz.Json/wiki) (it's not long)
