SELECT 
    c."Name",
    c."Alpha2Code",
	c."Alpha3Code",
	c."Region",
	c."SubRegion"
FROM db.public."Countries" AS c