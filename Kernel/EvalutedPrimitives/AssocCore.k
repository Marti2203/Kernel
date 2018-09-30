($define! assoc-core
($lambda (object alist)
($let ((alist (filter ($lambda (record)
(equal? object (car record)))
alist)))
($if (null? alist)
()
(car alist)))))