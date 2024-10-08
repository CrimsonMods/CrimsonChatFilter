# CrimsonChatFilter
`Server side only` mod to filter unwanted words (or urls) from chat. The sending user will see their messages unfiltered, but all other clients will.

## Installation
* Install [BepInEx](https://v-rising.thunderstore.io/package/BepInEx/BepInExPack_V_Rising/)
* Install [Bloodstone](https://github.com/decaprime/Bloodstone/releases/tag/v0.2.1)
* Extract _CrimsonChatFilter.dll_ into _(VRising server folder)/BepInEx/plugins_

* Run server to generate _(VRising server folder)/BepInEx/plugins/CrimsonChatFilter/filtered_words.json_
* Edit _filtered_words.json_ to modify the words that should be filtered

The Filtered Words list supports literals and wildcards for example

* "shit" will filter = "shit".
* "shit*" will filter = "shit", "shitface".
* "\*shit" will filter = "shit", "fuckshit".
* "\*shit*" will filter = "shit", "shitface", "fuckshit".

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

## Support

Want to support my V Rising Mod development? 

Donations Accepted with [Ko-Fi](https://ko-fi.com/skytech6)

Or buy/play my games! 

[Train Your Minibot](https://store.steampowered.com/app/713740/Train_Your_Minibot/) 

[Boring Movies](https://store.steampowered.com/app/1792500/Boring_Movies/)