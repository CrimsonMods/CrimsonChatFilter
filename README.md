# CrimsonChatFilter
`Server side only` mod to filter unwanted words (or urls) from chat. The sending user will see their messages unfiltered, but all other clients will.

## Installation
* Install [BepInEx](https://v-rising.thunderstore.io/package/BepInEx/BepInExPack_V_Rising/)
* Install [Bloodstone](https://github.com/decaprime/Bloodstone/releases/tag/v0.2.1)
* Extract _CrimsonChatFilter.dll_ into _(VRising server folder)/BepInEx/plugins_

* Run server to generate _(VRising server folder)/BepInEx/plugins/CrimsonChatFilter/filtered_words.json_
* Edit _filtered_words.json_ to modify the words that should be filtered

## Configurable Values
```ini
[CrimsonChatFilter]

## Enable or disable chat filtering
# Setting type: boolean
# Default value: true
EnableMode = true

## If enabled, others won't see the message, otherwise replaces filtered words with ****
# Setting type: boolean
# Default value: false
FullRemove = false

## Includes .com and server addresses in filter list
# Setting type: boolean
# Default value: true
FilterURLs = true
```