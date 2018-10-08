($define! assq-core
($lambda (object alist)
($let ((alist (filter ($lambda (record)
(eq? object (car record)))
alist)))
($if (null? alist)
()
(car alist)))))