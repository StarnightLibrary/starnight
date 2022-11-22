---
uid: starnight-internal-main
title: Starnight.Internal
---

# Starnight.Internal

If you've made your way down here, welcome in the depths of hell. If you've just stumbled upon this, you are advised to *leave. Now.*

Starnight.Internal is the package that directly wraps the Discord API, minus a handful of sanity changes exactly as outlined in the documentation. There is no handholding here and very little convenience features - you are exposed to the dangerous radiation of the Discord API. Hazmat suits are recommended.

There are very little reasons to use this, other than speed and potentially needing access to information not present in the main abstraction package.

If you do decide to dwell in this realm, to withstand the terrors of Tartarus ever-present, you have come to the right place.

## Validity and Accuracy

Starnight.Internal does not validate or sanitize any information. Requests are taken at face value and passed to the Discord API directly, with the exception of rate-limiting to ensure Discords ratelimits are never hit during correct operation.

> [!CAUTION]
> This also means that 4xx error codes caused by user error will not be prevented by the library and will be counted towards your error threshold. Make sure to validate and sanitize data as you see fit before making API requests.

On the other hand, Starnight.Internal returns exactly what Discord returns without post-processing, which may be considered beneficial. This also means that features are available sooner, breaking changes may occur more frequently and that the library may see more of Discord testing-in-production.