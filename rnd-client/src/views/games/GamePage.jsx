import ItemContainer from "../../components/containers/item/ItemContainer";
import {useState} from "react";

export default function GamePage () {
  const [game] = useState(mock)

  return (
    <ItemContainer data={game} {...metadata}/>
  );
}

const mock = {
  name: "mrak",
  title: "Мрак",
  subtitle: "AndrewSolo",
  image: "https://cdn.discordapp.com/attachments/1104404469090881556/1113971170581155900/c0a31dc92f19f998.png",
  description: "Это темное и пустое место. Здесь не Светит солнце, а все вдалеке от священного света пропитано мраком. Заразительная скверна покрывает живое и неживое, уродуя и изменяя.",
  wiki: "https://rockndice.gitbook.io/wiki/mrak",
  created: "22 Января 2022 г."
}

const metadata = {
  fields: [
    {
      name: "name",
      label: "Имя",
      type: "text",
      visible: false,
      editable: true,
    },
    {
      name: "title",
      label: "Название",
      type: "text",
      visible: false,
      editable: true,
    },
    {
      name: "description",
      label: "Описание",
      type: "text",
      visible: true,
      editable: true,
    },
    {
      name: "created",
      label: "Создано",
      type: "text",
      visible: true,
      editable: false,
    },
    {
      name: "wiki",
      label: "Википедия",
      type: "text",
      visible: false,
      editable: true,
    }
  ],
  actions: [
    {
      name: "invite",
      label: "Пригласить",
      href: "members/@new"
    },
    {
      name: "wiki",
      label: "Вики",
      href: "https://rockndice.gitbook.io/wiki/mrak"
    }
  ]
}