const url = "http://localhost:5139/api/NotaFiscal";
// let notas = [];

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
    // pega os valores
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
        .then(response => {
            if (!response.ok) {
                return response.json().then(err => {
                    throw new Error(JSON.stringify(err));
                });
            } else {
                listNotas();
                document.getElementById("cadastrada").innerHTML = "Nota cadastrada com sucesso.";
            }
        })
        .catch(error => {
            document.getElementById("cadastrada").innerHTML = "Erro ao cadastrar a nota fiscal: <br>" + error;
        })
}

function _showNotes(notas) {
    document.getElementById("preencher").classList.add("some");
    let quadro = document.getElementById("notas_div");
    notas.forEach(nota => {
        quadro.innerHTML += 
            `<div class="nota_fiscal"> <p class="id_bg"> <span style="font-weight: bold;"> Id da nota: </span> ${nota.notaFiscalId} </p>` +
            `<p> <span style="font-weight: bold;"> Número: </span> ${nota.numeroNf} </p>` + 
            `<p> <span style="font-weight: bold;"> Valor: </span> R$ ${nota.valorTotal} </p>` +
            `<p> <span style="font-weight: bold;"> Data de emissão: </span> ${nota.dataNf} </p>` +
            `<p> <span style="font-weight: bold;"> CNPJ Emissor: </span> ${nota.cnpjEmissorNf} </p>` +
            `<p> <span style="font-weight: bold;"> CNPJ Destinatário: </span> ${nota.cnpjDestinatarioNf} </p> </div>`
    });
}

function listAll() {
    let fetchUrl = `${url}/listarNotas`;
    fetch(fetchUrl)
        .then(response=> {
            if (!response.ok) {
                _showNotes(null)
                throw new Error("Erro ao checar todas as notas.");
            }
            return response.json();
        })
        .then(data => _showNotes(data))
        .catch(error => console.error("erro.", error));
}

function listarDestinatario() {
    let cnpj = document.getElementById("cnpj_do_destinatario").value;
    cnpj = cnpj.replace(/[.\-/]/g, '');
    let fetchUrl = `${url}/listarPorDestinatario/${cnpj}`;
    fetch(fetchUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error("CNPJ do destinatário não encontrado.");
            }
            return response.json();
        })
        .then(data => _showNotes(data))
        .catch(error => console.error("erro: ", error));
}

function listarEmissor() {
    let cnpj = document.getElementById("cnpj_do_emissor").value;
    cnpj = cnpj.replace(/[.\-/]/g, '');
    let fetchUrl = `${url}/listarPorEmissor/${cnpj}`;
    fetch(fetchUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error("CNPJ do emissor não encontrado.");
            }
            return response.json();
        })
        .then(data => _showNotes(data))
        .catch(error => console.error("erro: ", error));
}