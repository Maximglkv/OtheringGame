->main 
   #speaker:Player #portrait:player_normal #layout:left
===main===
    + [Dad? I'm home.]->FirstChoice 
        Dad? I'm home. #speaker:Player #portrait:player_normal #layout:left
    +[Say nothing]->FirstChoice
    You say nothing 
===FirstChoice===
father looks up, trying to smile despite his obvious discomfort. #speaker:  #portrait:Empty #layout:right home

Hey, sweetheart. How was your day? #speaker:Parent #portrait:parent_sad #layout: right


+[Okay. But that doesn't matter right now. How are you feeling?]->SecondChoice
Okay. But that doesn't matter right now. How are you feeling? #speaker:Player #portrait:player_normal #layout:left
+[Not good but I'm home now How are you]->SecondChoice
Not good but I'm home now How are you #speaker:Player #portrait:player_sad #layout:left
===SecondChoice===
you rush to his side, checking his pillboxes.


I'm alright. Just a bit under the weather. #speaker:Parent #portrait:parent_sad #layout: right


Dad, you look pale. Why didn't you call me earlier? #speaker:Player #portrait:player_sad #layout:left


Didn't want to bother you, I'm sorry #speaker:Parent #portrait:parent_sad #layout: right


Dad don't worry about it, I'll always be here for you. #speaker:Player #portrait:player_sad #layout:left


Mom's not home til later let me check what needs to be done #speaker:Player #portrait:player_sad #layout:left

father watches her, a mix of gratitude and worry in his eyes.#speaker:  #portrait:Empty #layout:right home


I'm sorry for being a burden.#speaker:Parent #portrait:parent_sad #layout: right



(chuckles weakly)
Guess the roles reverse as we grow older.#speaker:Parent #portrait:parent_happy #layout: right
reaches for her hand, giving it a weak squeeze.#speaker:  #portrait:Empty #layout:right home
-> END
