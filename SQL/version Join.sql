--registry = регистрацией клиентов
--shopping = купленные товары
--[firstday; lastday] = все дни предыдущего месяца

declare @firstday date, @lastday date
set @firstday = CONVERT(date, DATEADD(DAY, 1-DAY(DATEADD(mm, -1, GETDATE())), DATEADD(mm, -1, GETDATE())))
set @lastday = CONVERT(date, DATEADD(DAY, -1, DATEADD(MONTH, 1, @firstday)))

SELECT DISTINCT Name FROM regist r INNER JOIN shopping s ON (r.CustomerId = s.CustomerId and s.ProductName = N'молоко')
LEFT OUTER JOIN shopping scr ON (s.CustomerId = scr.CustomerId AND scr.ProductName = N'сметана'
								AND 1 = (
								case when (CONVERT(date, scr.PurchaiseDatetime) <= @lastday AND CONVERT(date, scr.PurchaiseDatetime) >= @firstday) 
								then 1 else 0 end)
								)
WHERE scr.CustomerId IS NULL