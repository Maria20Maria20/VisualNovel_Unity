VAR characterName = "Anna"
VAR characterExpression = 0


Привет, как дела?
~characterExpression = 1
Привет, как дела?1
Привет, как дела?2
~characterName = "Bel"
Привет, как дела?3
~characterExpression = 2
~characterName = ""
Привет, как дела?4
    +[Норм]
        ->continue
    +[Не норм]
        ->continue
=== continue ===
Привет, как дела?1
Привет, как дела?2
Привет, как дела?3
Привет, как дела?4  
~characterExpression = 1
Привет, как дела?4
    +[Хорошо]
        ->answer
    +[Не хорошо]
        ->answer2
->END
=== answer ===
Окей
~characterName = "Bel"
~characterExpression = 2
Привет, как дела?
->END
=== answer2 ===
~characterName = "Anna"
~characterExpression = 2
Привет, как дела?
->END