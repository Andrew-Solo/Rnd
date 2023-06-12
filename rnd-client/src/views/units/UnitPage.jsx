import {useState} from "react";
import UnitHeader from "./UnitHeader";
import {Box, Stack} from "@mui/material";
import UnitsStack from "./UnitsStack";
import ActionsContainer from "./actions/ActionsContainer";

export default function UnitPage () {
  const [data] = useState(mock);
  const [editing, setEditing] = useState(false);
  const {fields, actions} = metadata;

  return (
    <Box component="main" width={1} display="flex" flexDirection="column">
      <UnitHeader title={data.title} subtitle={data.subtitle} image={data.image}/>
      <Box padding={4}>
        <Stack spacing={4} maxWidth={750}>
          <Box display="flex" gap={4}>
            <Box width={7.5/10} minWidth={150}>
              <UnitsStack data={data} fields={fields} editing={editing}/>
            </Box>
            <Box width={2.5/10} minWidth={100}>
              <ActionsContainer data={data} actions={actions} editing={editing} setEditing={setEditing}/>
            </Box>
          </Box>
        </Stack>
      </Box>
    </Box>
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