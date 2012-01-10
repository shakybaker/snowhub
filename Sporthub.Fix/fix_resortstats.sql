INSERT INTO ResortStats
                         (ResortID)
SELECT        ID
FROM            Resort
WHERE        (ID NOT IN
                             (SELECT        ResortID
                               FROM            ResortStats AS ResortStats_1))