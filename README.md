# Kuo

Kuo es un servicio de windows para la gestión del dispositivo Biometrico ZK-F19 ID. Como puente de cominicación el servicio instancia un servidor socket que es el encargado de emitir y de recibir data.

## Prerequisitos
1. Es necesario .NET framework 4.5
2. Tener instalado Visual Studio Installer, este se puede obtener mediante la busqueda de plugins y extensiones de Visual Studio.
3. Es necesario Visual Studio 2015 Comunity Edition, para un mejor rendimiento, en compilación.

## Instalación

### Modo Desarrollo
En este modo simplemente se corre el sevicio en modo debug por Visual Studio y queda listo para ser consumido.

### Modo Procucción
Se brinda un instalador, en el siguiente enlace; [kuo Installer](http://www.google.com)

## API

### Conexión Socket
WebSocket es un protocolo que al igual que http es usado para la comunicación entre el cliente y servidor, pero en este caso, http solo functiona en una sola dirección; el cliente realiza una petición y el servidor responde. Mientras que con sockets se abre un canal de comunicación en el que el servidor emite data cada ves que algo cambia.

Para crear una sencilla conexión websocket desde JavaScript, es necesario crear la siguiente estancia.

```js
	var kuo = new WebSocket('ws://127.0.0.1:2012')
```

### Ajustando La libreria en el frontend
Acontinuación se crea una funcionalidad con el que se pretende emitir data al servidor.

```js

	function KuoS(){} // Clase KuoS
    
    // funcion emit de la clase KuoS
    KuoS.prototype.emit = function(event, data) {
        var msg = {
            type: event,
            payload: []
        };
        
    	if(typeof data == 'object') {
           msg.payload.push(data)
        }
    	
    	kuo.send(JSON.stringify(msg));
    }

```

### Emitiendo Eventos
Una ves conectado el socket a nuestro servidor socket, que se encuentra corriendo como servicio de windows. podemos mandarle mensajes, para que este los interprete, lo que deja por consiguiente cada uno de los siguientes mensajes a emitir.

pero antes de entender los eventos es necesario instanciar la nueva clase KuoS de la siguiente forma.

```js
	var ko = new KouS();
```

#### reconnect
Permite reconectar el dispositivo Biometrico, no espera respuesta del callback.

```js
	ko.emit("reconnect");
```

#### recordcard
Permite registrar un carnet en el dispositvo biometrico.

```js
	ko.emit("recordcard", 
        { 
            user: String("myUserId"), 
            name: String("myName"), 
            card: String("myCardNumber") 
        }
    );
```

### recordfinger
Permite grabar una huella en el dispositivo biometrico. Tambien prepara al dispositivo y lo deja en este modo de guardado.
```js
	ko.emit("recordfinger", { user:  String("myUserId") });
```

### getfinger
Recupera la huella de un usuario especifico, grabado en el dispositivo biometrico.

```js
	ko.emit("getfinger", { user:  String("myUserId") });
```


### default
En caso de emitirse un evento no registrado, ejemplo;
```js
	ko.emit("pepe");
```
El callback encargado de interpretar, estos eventos simplemente sera ignorado, y retornara un mensaje como el siguiente:

```js
	var foo = "Evento No Registrado";
```

## Recibiendo data
Ahora que ya hemos hablado de como el cliente emite X cantidad de eventos, es el turno del servidor y de como recibe la data el cliente.

```js
kuo.onmessage = function (evt) {
      console.log(evt.data);
}
```

donde ```"evt"``` contiene toda la data emitida por el servidor.

### getfinger
En el caso de haber emitido un ```"getfinger"```, la data que recibe el cliente es la siguiente.

```json
{
    "type": "getfinger",
    "payload": [
        {
            "user"  : "user",
            "index" : "index",
            "data"  : "data",
            "length": "length"
        }
    ] 
}
```

### Eventos del Biometrico.

#### card

En caso de que no exista un usuario en el dispositivo con la tarjeta detectada, el servidor emite lo siguiente:

```json
{
    "type"   : "regcard",
    "payload": {
        "card": "myNumberCard"
    } 
}
```

En caso de que exista un usuario en el dispositivo biometrico se emite lo siguiente:


1. Primer retorno
```json
{
    "type"   : "regcard",
    "payload": {
        "card": "myNumberCard"
    } 
}
```

2. Segundo retorno
```json
{
    "type"   : "card",
    "payload": {
        "user": "myUserId"
    }
}
```

En este caso solo necesitmaos el segundo retorno. podemos ignorar siempre el primer retorno.

#### Fingerprint
Cuando existe un usuario en el dispositivo biometrico, con la huella especificada, el servidor retorna lo siguiente:

```json
{
    "type"   : "finger",
    "payload": {
        "user": "myUserId"
    }
}
```
