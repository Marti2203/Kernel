($define! append-core!
    ($lambda lss
        ($define! set-last!
            ($lambda (ls tail)
                ($let ((next (cdr ls)))
                    ($if (pair? next)
                        (set-last! next tail)
                        (set-cdr! ls tail)))))
        (map 
            ($lambda (x) (apply set-last! x))
            (list-neighbors 
                (filter ($lambda (x) (not? (null? x))) lss)))
        #inert))