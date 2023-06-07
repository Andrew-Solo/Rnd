import {Box, Button, Stack} from "@mui/material";
import ItemHeader from "./ItemHeader";
import Field from "../fields/Field";
import {useState} from "react";

export default function ItemPanel({data, fields, actions}) {
  const [editing, setEditing] = useState(false);

  return (
    <Box component="main" width={1} display="flex" flexDirection="column">
      <ItemHeader title={data.title} subtitle={data.subtitle} image={data.image}/>
      <Box padding={4}>
        <Stack spacing={4} maxWidth={750}>
          <Box display="flex" gap={4}>
            <Box width={7.5/10} minWidth={150}>
              <Stack spacing={2}>
                {fields
                  .filter(field => editing ? field.editable : field.visible)
                  .map(field => <Field key={field.name} editing={editing} value={data[field.name]} {...field}/>)}
              </Stack>
            </Box>
            <Box width={2.5/10} minWidth={100}>
              <Stack spacing={2}>
                {editing
                  ? <>
                    <Button variant="contained" onClick={() => setEditing(!editing)}>
                      Сохранить
                    </Button>
                    <Button variant="contained" color="secondary" onClick={() => setEditing(!editing)}>
                      Отмена
                    </Button>
                    <Button variant="contained" color="error">
                      Удалить
                    </Button>
                  </>
                  : <Button variant="contained" onClick={() => setEditing(!editing)}>Редактировать</Button>
                }
                {actions.filter(() => !editing).map(action =>
                  <Button variant="contained" color="secondary" key={action.name} href={action.href}>
                    {action.label}
                  </Button>
                )}
              </Stack>
            </Box>
          </Box>
        </Stack>
      </Box>
    </Box>
  );
}