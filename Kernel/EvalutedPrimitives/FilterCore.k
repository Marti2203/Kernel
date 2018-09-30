($define! filter-core
($lambda (accept? ls)
(apply append
(map ($lambda (x) ($if (apply accept? (list x)) (list x) ())) ls))))