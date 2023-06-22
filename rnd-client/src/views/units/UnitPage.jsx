import UnitHeader from "./UnitHeader";
import {Box} from "@mui/material";
import UnitsGrid from "./cards/CardsGrid";
import {observer} from "mobx-react-lite";

const UnitPage = observer(({unit}) => {
  return (
    <Box component="main" width={1} padding={4} gap={4} display="flex" flexDirection="column">
      <UnitHeader title={unit.title}/>
      <UnitsGrid tokens={[]}/>
    </Box>
  )
});

export default UnitPage