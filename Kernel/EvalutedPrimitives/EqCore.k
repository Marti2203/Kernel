($define! eq-core?
($let ((old-eq? eq?))
($lambda x
($if ($and? (pair? x) (pair? (cdr x)) (null? (cddr x)))
(apply old-eq? x)
(apply and?
(map ($lambda (x) (apply old-eq? x))
(list-neighbors x)))))))