;and-core
($define! and-core?
($lambda x
($define! aux
($lambda (x k)
($cond ((<=? k 0)
((car x)
(#t
#t)
(aux (cdr x) (- k 1)))
#f))))
(aux x (car (get-list-metrics x)))))