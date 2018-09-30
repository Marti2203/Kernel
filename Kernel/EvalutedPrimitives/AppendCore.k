($define! append-core
($lambda lss
($define! set-last!
($lambda (ls tail) ; set cdr of last pair of ls to tail
($let ((next (cdr ls)))
($if (pair? next)
(set-last! next tail)
(set-cdr! ls tail)))))
($define! aux2
($lambda (ls tail) ; prepend ls onto tail
($if (null? ls)
tail
(cons (car ls) (aux2 (cdr ls) tail)))))
($define! aux1
($lambda (k lss tail) ; prepend k elmts of lss onto tail
($if (>? k 0)
(aux2 (car lss)
(aux1 (- k 1) (cdr lss) tail))
tail)))
($if (null? lss)
()
($let (((#ignore #ignore a c)
(get-list-metrics lss)))
($if (>? c 0)
($let ((cycle (aux1 c (list-tail lss a) ())))
($cond ((pair? cycle)
(set-last! cycle cycle)))
(aux1 a lss cycle))
(aux1 (- a 1) lss (list-ref lss (- a 1))))))))