;apply-core
($define! apply
($lambda (appv arg . opt)
(eval (cons (unwrap appv) arg)
($if (null? opt)
(make-environment)
(car opt)))))