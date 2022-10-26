# Question 1

## Result

``` console
    Son Tung MTP. Age: 25
    Huan Rose. Age: undefined
    false
    Son Tung MTP. Age: 25
    Son Tung MTP. Age: 25
```

2 last `console.log`(s) are able to print `'Tung Nui'` for `person.name`

## Explain

1. Normal case
2. When call function with missing values, those `params` or `props` are set to `undefined` by default.
3. When compare by `===`, 2 objects with same props are different.
4. When set value for a prop not defined before, it is added to object automatically.
5. Spread all props of an object to another new object by `newObj = { ...obj, newProp: value }`
