SELECT
    c."Id",
    c."Code",
    c."Name",
    c."Symbol"
FROM db.public."Currencies" AS c
ORDER BY c."Name"