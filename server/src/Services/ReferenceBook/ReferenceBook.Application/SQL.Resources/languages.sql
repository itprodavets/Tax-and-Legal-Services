SELECT
    l."Id",
    l."Name",
    l."NativeName",
    l."Code"
FROM db.public."Languages" AS l
ORDER BY l."Name"