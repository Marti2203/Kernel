($define! equal-core?
($let ((old-equal? equal?))
($lambda x
($if ($and? (pair? x) (pair? (cdr x)) (null? (cddr x)))
(apply old-equal? x)
(apply and?
(map ($lambda (x) (apply old-equal? x))
(list-neighbors x)))))))