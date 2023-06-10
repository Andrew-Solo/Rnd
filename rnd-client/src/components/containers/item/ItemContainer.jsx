import {Box, Stack} from "@mui/material";
import ItemHeader from "./ItemHeader";
import {useState} from "react";
import FieldsContainer from "../../fields/FieldsContainer";
import ActionsContainer from "../../actions/ActionsContainer";

export default function ItemContainer({data, fields, actions}) {
  const [editing, setEditing] = useState(false);

  return (
    <Box component="main" width={1} display="flex" flexDirection="column">
      <ItemHeader title={data.title} subtitle={data.subtitle} image={data.image}/>
      <Box padding={4}>
        <Stack spacing={4} maxWidth={750}>
          <Box display="flex" gap={4}>
            <Box width={7.5/10} minWidth={150}>
              <FieldsContainer data={data} fields={fields} editing={editing}/>
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