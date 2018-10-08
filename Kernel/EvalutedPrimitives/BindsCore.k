($define! $binds-core?
    ($vau (exp . ss) dynamic
        (guard-dynamic-extent 
            ()
            ($lambda ()
            ($let 
                ((env (eval exp dynamic)))
                (map ($lambda (sym) (eval sym env)) ss))
            #t)
            (list (list error-continuation ($lambda (#ignore divert) (apply divert #f)))))))