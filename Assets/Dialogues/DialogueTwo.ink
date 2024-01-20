VAR characterName = "Anna"
->label1
=== label1 ===
1
2
3
4
5
Ты перешёл на вторую главу?
    + Нет
        ->knotName
    + Да
        ->knotName

-> END
=== knotName ===
This is the content of the knot.
-> END

