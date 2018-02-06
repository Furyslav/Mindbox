--registry = регистрацией клиентов
--shopping = купленные товары
--[firstday; lastday] = все дни предыдущего месяца

declare @firstday date, @lastday date
set @firstday = CONVERT(date, DATEADD(DAY, 1-DAY(DATEADD(mm, -1, GETDATE())), DATEADD(mm, -1, GETDATE())))
set @lastday = CONVERT(date, DATEADD(DAY, -1, DATEADD(MONTH, 1, @firstday)))

SELECT DISTINCT Name FROM regist r
WHERE EXISTS (SELECT * FROM shopping WHERE CustomerId = r.CustomerId AND ProductName = N'молоко')
AND NOT EXISTS (SELECT * FROM shopping WHERE CustomerId = r.CustomerId AND ProductName = N'сметана' 
				AND 1 = (
								case when (CONVERT(date, PurchaiseDatetime) <= @lastday AND CONVERT(date, PurchaiseDatetime) >= @firstday) 
								then 1 else 0 end)
								);