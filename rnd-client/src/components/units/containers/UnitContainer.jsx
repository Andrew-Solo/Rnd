import {Box, Stack} from "@mui/material";
import UnitHeader from "./UnitHeader";
import {useState} from "react";
import ActionsContainer from "../../actions/ActionsContainer";
import UnitsStack from "../../units/containers/UnitsStack";

export default function UnitContainer({data, fields, actions}) {
  const [editing, setEditing] = useState(false);

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