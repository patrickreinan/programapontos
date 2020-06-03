# ProgramaPontos

Aplicação simples para demonstração da utilização de CQRS + EventSourcing.

Esta aplicação tem a finalidade de mostrar através de uma regra de programa de pontos como o CQRS + EventSourcing pode ser implementado. O domínio foi modelado utiizando o Domain Driven Design (DDD).

Nesta aplicação está sendo utilizado:

<ul>
<li><b>RabbitMQ</b> como serviço de mensageria</li>
<li><b>Elastic Search</b> como base leitura (Read Model)</li>
<li><b>MongoDB</b> como EventStore</li>
</ul>

<ul>
<li>02-06-2020 <b>Adicionado docker-compose</b></li>
<li>14-02-2020 <b>Ajustes de performance</b></li>  
<li>09-02-2020 <b>Upgrade para .NET Core 3.1</b></li>
<li>01-06-2019 <b>gRPC adicionado</b></li>
<li>07-04-2019 <b>Snapshot adicionado</b></li>
</ul>
