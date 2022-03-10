# Starnight error codes

Starnight employs an *internal* system of error codes to propagate errors from Discords API. Errors caused by faults in Starnight's logic do not use error codes.

## Discord REST API error codes

Note: All exceptions are found in `Starnight.Exceptions`.

| HTTP response code | Starnight error code | Library exception type |
| ------------------ | -------------------- | ---------------------- |
| 400 | 10000 | `DiscordInvalidRequestException` |
| 401 | 10001 | `DiscordMissingOrInvalidTokenException` |
| 403 | 10002 | `DiscordUnauthorizedException` |
| 404 | 10003 | `DiscordNotFoundException` |
| 405 | 10004 | `DiscordInvalidRequestException` |
| 413 | 10005 | `DiscordOversizedPayloadException` |
| 429 | 10006 | `DiscordRatelimitHitException` |
| 500 | 10007 | `DiscordServerErrorException` |
| 502 | 10008 | `DiscordServerErrorException` |
| 503 | 10009 | `DiscordServerErrorException` |
| 504 | 10010 | `DiscordServerErrorException` |
| - | - | - |
| 429 | 10011 | `DiscordRatelimitHitException` |
| 0 | 10012 | `DiscordServerErrorException` |

Note 1: The error code 10011 is caused by the internal ratelimiter, not by Discord's ratelimiter, and should be treated accordingly. 429 is still attached to the response.

Note 2: The error code 10012 is caused by undocumented Discord behaviour. Please report all 10012 errors to the library developers.
