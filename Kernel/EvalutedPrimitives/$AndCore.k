($define! $and-core?
($vau x e 
($cond ( (null? x) #t )
       ( (null? (cdr x) ) (eval (car x) e) )
       ( (eval (car x) e) (apply (wrap $and-core?) (cdr x) e)) 
       ( #t $f)
       )))
