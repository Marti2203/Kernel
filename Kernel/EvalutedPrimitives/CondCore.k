($define! $cond-core
($vau clauses env
($define! aux
($lambda ((test . body) . clauses)
($if (eval test env)
(apply (wrap $sequence) body env)
(apply (wrap $cond) clauses env))))
($if (null? clauses)
#inert
(apply aux clauses))))