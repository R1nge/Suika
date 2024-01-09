TODO:
Mod slot - use mod name to switch

Hide continue button if mod suika images have changed (are different)

(Actually, there's a way of detection change in files, but it would be overkill???)
((Use sha1 algo on files to get a hash, store its encrypted version on a disk, if current config.json file hash is different - reset continue))
(Instead, I can reset the continue data, when another mod has been selected)



Shake OR Spin ???
Detect edge lines (shape) in the modding tool ???

UI:
Add animations
Leader board (local)
Evolution line (use suika icons)
Show error message/error icon, when mod is invalid??? Mod will use 'default' assets if some are missing


Fix a lag spike when adding a polygon collider to a sprite renderer