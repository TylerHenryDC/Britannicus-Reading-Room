CREATE PROCEDURE Inventory_Sort @Sort int
as

SELECT iv.INVENTORY_ID, it.Item_Name, it.Item_Name, it.Item_Description, ica.CATEGORY_NAME, it.ISBN, it.Item_Author, it.Item_Language, it.Retail_Price, ic.Item_Condition, iv.SALES_Price, iv.Quantity FROM Inventory as iv 

LEFT JOIN ITEM as it 

ON iv.Item_ID = it.Item_ID 

LEFT JOIN ITEM_CATEGORY as ica

ON ica.CATEGORY_ID = it.ITEM_CATEGORY_ID

LEFT JOIN Item_Condition as ic 

ON iv.Item_Condition_ID = ic.Item_Condition_ID 
ORDER BY 

CASE @Sort 

WHEN 1 THEN it.Item_Name 

WHEN 2 THEN it.Item_Category_ID 

WHEN 3 THEN	it.Item_Author 

WHEN 4 THEN	it.Item_Language 

WHEN 5 THEN	it.Retail_Price 

WHEN 6 THEN	iv.Item_Condition_ID 

WHEN 7 THEN	iv.Quantity 

WHEN 8 THEN	it.Retail_Price 

END DESC;