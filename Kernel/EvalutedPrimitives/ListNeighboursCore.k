($define! list-neighbors-core
($lambda (ls)
($define! aux
($lambda (ls n) ; get n sets of neighbors from ls
($if (>? n 0)
(cons (list (car ls) (cadr ls))
(aux (cdr ls) (- n 1)))
())))
($let (((p #ignore a c)
($if (=? c 0)
(get-list-metrics ls)))
85(aux ls (- a 1))
($let ((ls (aux ls p)))
(encycle! ls a c)
ls)))))