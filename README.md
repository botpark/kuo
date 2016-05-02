# KUO

## ¿Que es Kou?
Es un servicio de windows, que gestiona un servidor sockect para el manejo e interacción de eventos del dispositivo biometrico ZK-F19 ID.

## CLIENTE

1. Crear una estancia de conexion mediante el protocolo WebSocket, que puentea la comunicación entre el cliente y el servicio windows que corre de lado del servidor.

2. Estancia del Socket
```js
var socket = new WebSocket('ws://127.0.0.1:2012')
```

3. Se crea el receptor de mensajes en el cliente. donde "evt" es el mensaje que emite el servidor.

```js
socket.onmessage = function (evt) {
      console.log(evt.data);
}
```

4. se crea el emisor para  enviar datos al servicio de windows.

* La estructura del msj esta estructurada en un type y un payload.
* type: Es el tipo de acción que ejecutara el lector ZK-F9 ID.
* payload: Es la información requerida para ejecutar cada type. 

```js
	function emit(event, data) {
		var msg = {
			type: event,
			payload: []
		};
		msg.payload.push(data)
		socket.send(JSON.stringify(msg));
	} 
```

3.1 La data que se envia viente con 
3.1.1 Incribir una huella dactilar.

```js
	emit('fingerin', { user:  String(record.data.ID)})
```

	3.1.2 Recuperar la huella del lector.
```js
       emit('fingerout', { user:  String(record.data.ID)})
```
	3.1.3 Registrar un carnet.
```js
	   emit('cardin', { user: String(HID_USER.getValue()), name: String(HNOMBRE_USER.getValue()), card: String(record.data.ETIQ_ETIQUETA) })
```

## SERVIDOR

* Se dejan los siguientes atributos como estaticos en el servicio de windows.

```c#

	// Ip del dispositivo biometrico
	private const string _IP_BIOMETRICO = "192.168.1.201";
	
	// Puerto del socket en que escucha conexiones
	private const string _PORT_SOCKET = "2012";
	
```
* La ip del socket se toma por defecto como "127.0.0.1" ó "localhost"

### METODOS Y EVENTOS DEL LECTOR

#### METODOS 
 
```c#
	public void CapturarHuella(String usuarioID) {}
```
```c#
	public string RecuperarHuella(string usuarioID){} 
```
```c#
	public void RegistrarCarnet(string Carnet, string Nombre, string usuarioID) {}
```

#### EVENTOS DEL LECTOR
```c#
	private void GetCard(int card) {}
```

```c#
	private void GetType
	(
		string a, 
		int b, 
		int c, 
		int d, 
		int e, 
		int f, 
		int g, 
		int h, 
		int i, 
		int j, 
		int k
	){}
```

