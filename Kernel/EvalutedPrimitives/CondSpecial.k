;cond-special
($define! $cond-special
($vau clauses env
($if (null? clauses)
#inert
($let ((((test . body) . clauses) clauses))
($if (eval test env)
(apply (wrap $sequence) body env)
(apply (wrap $cond) clauses env))))))