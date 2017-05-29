
# Token based authentication in WebAPI

In questa demo viene illustrato come implementare l'autenticazione basata su token utilizzando ASPNet Core.
L'autenticazione basata su token è diventata lo standard de-facto per le single-page application (SPA).

E' possibile passare il token di autenticazione tramite header o tramite cookie.
Solitamente per le applicazione mobile viene usato l'header, mentre per le web applicaziont è possibile utilizzare i cookie.

In questa demo il token viene passato nell'header della richiesta:

```
key            value
-------------  ----------------------
Authorization: Bearer MY_TOKEN
```
(attenzione allo spazio tra Bearer e il token)

Per ottenere il token effettuare una chiamata HTTP POST su /api/token passando username e passaword

```
POST api/token
Content-Type: application/x-www-form-urlencoded
username=TEST&password=TEST123
```

E' possibile decodificare il token JWT utilizzando l'utility https://www.jsonwebtoken.io/


### Riferimenti:

*   https://samueleresca.net/2016/12/developing-token-authentication-using-asp-net-core/
*   https://stormpath.com/blog/token-authentication-asp-net-core
