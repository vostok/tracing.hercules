# Vostok.Tracing.Hercules

[![Build status](https://ci.appveyor.com/api/projects/status/github/vostok/tracing.hercules?svg=true&branch=master)](https://ci.appveyor.com/project/vostok/tracing.hercules/branch/master)
[![NuGet](https://img.shields.io/nuget/v/Vostok.Tracing.Hercules.svg)](https://www.nuget.org/packages/Vostok.Tracing.Hercules)

An implementation of trace span sender based on Hercules client. Also provides mapping from Hercules events back to spans.

Here's how [ISpan](https://github.com/vostok/tracing.abstractions/blob/master/Vostok.Tracing.Abstractions/ISpan.cs) instances are mapped into Hercules events:

- `TraceId` (mandatory) ---> `TraceId` tag of `UUID` type.

- `SpanId` (mandatory) ---> `SpanId` tag of `UUID` type.

- `ParentSpanId` (optional) ---> `ParentSpanId` tag of `UUID` type.

- `BeginTimestamp` (mandatory) corresponds to 2 tags:
  - `BeginTimestampUtc` — a `long` tag that contains the UTC timestamp in 100-ns ticks from Gregorian Epoch.
  - `BeginTimestampUtcOffset` — a `long` tag with offset from UTC expressed in 100-ns ticks.

- `EndTimestamp` (optional) also corresponds to 2 tags, both of which can be absent for 'endless' spans:
  - `EndTimestampUtc` — a `long` tag that contains the UTC timestamp in 100-ns ticks from Gregorian Epoch.
  - `EndTimestampUtcOffset` — a `long` tag with offset from UTC expressed in 100-ns ticks.
  
- `Annotations` dictionary corresponds to a container with same name. This container contains a tag for each pair. Keys are translated as-is, and the values are handled according to following conventions:
  - If the value is a primitive scalar or a vector of primitive scalars natively supported by Hercules (such as `int`, `long`, `guid`, `string`, etc), it's mapped as-is. 
  - Otherwise the value gets converted to `string`: either stringified directly (if it properly overrides `ToString()`) or serialized to JSON. No further container-like structure is allowed, all values end up being 'flat'.
  
Hercules event's built-in timestamp is chosen equal to `EndTimestampUtc` or `BeginTimestampUtc` if former is missing.

Gregorian epoch used as a reference point for timestamp is `1582-10-15T00:00:00.000Z`.
