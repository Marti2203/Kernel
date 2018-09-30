($define! countable-list-core?
($lambda args
(apply and?
(map ($lambda (x)
($let (((#ignore n #ignore c)
(get-list-metrics x)))
($or? (>? c 0)
(>? n 0))))
args))))