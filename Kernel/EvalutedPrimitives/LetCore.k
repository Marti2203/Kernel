;let-core
($define! $let-core
($vau (bindings . body) env
(eval (cons (list* $lambda (map car bindings) body) (map cadr bindings)) env)))