const url = "http://localhost:5139/api/NotaFiscal";
let notas = [];

function _displayItems(data) {
    if (!data) {
        document.getElementById("numero").value = "";
        document.getElementById("valor").value = "";
        document.getElementById("data").value = "";
        document.getElementById("cnpjEmissor").value = "";
        document.getElementById("cnpjDestinatario").value = "";
        return;   
    }

    document.getElementById("numero").value = data.numeroNf;
    document.getElementById("valor").value = data.valorTotal;
    document.getElementById("data").value = data.dataNf;
    document.getElementById("cnpjEmissor").value = data.cnpjEmissorNf;
    document.getElementById("cnpjDestinatario").value = data.cnpjDestinatarioNf;
    document.getElementById("encontrada").classList.remove("some");
    document.getElementById("nao_encontrada").classList.add("some");
}

function listNotas() {
    fetch(url)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error("Nota não encontrada.", error))
}

function buscarId() {
    let id = document.getElementById("id_nota").value;

    let fetchUrl = `${url}/buscarid/${id}`;

    fetch(fetchUrl)
        .then(response => {
            if (!response.ok) {
                _displayItems(null);
                document.getElementById("nao_encontrada").classList.remove("some");
                document.getElementById("encontrada").classList.add("some");
                throw new Error("ID não encontrado.");
            }
            return response.json();
        })
        .then(data => _displayItems(data))
        .catch(error => console.error("Erro ao buscar id", error));
}

function addNota() {
    const numero = document.getElementById("numero").value;
    const valor = document.getElementById("valor").value;
    const data = document.getElementById("data").value;
    const cnpjEmissor = document.getElementById("cnpjEmissor").value;
    const cnpjDestinatario = document.getElementById("cnpjDestinatario").value;

    const nota = {
        Numero: numero,
        Valor: valor,
        Data: data,
        CnpjEmissor: cnpjEmissor,
        CnpjDestinatario: cnpjDestinatario,
    };

    fetch(url, {
        method: "POST",
        headers: { 
            "Accept": "application/json",
            "Content-type": "application/json"  
        },
        body: JSON.stringify(nota)
    })
        .then(response => response.json())
        .then(() => {
            listNotas();
        })
        .catch(error => console.error("Não foi possível adicionar a nota.", error))
}