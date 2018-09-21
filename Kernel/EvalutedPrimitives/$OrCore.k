($define! $or-core?
($vau x e
($cond ((null? x) #t)
       ((null? (cdr x)) (eval (car x) e)) ; tail context
       ((eval (car x) e) (apply (wrap $or?) (cdr x) e)) 
       (#t #f)
)))