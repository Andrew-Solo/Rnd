import ModuleHeader from "./ModuleHeader";
import {Box} from "@mui/material";
import UnitsGrid from "./cards/CardsGrid";
import {observer} from "mobx-react-lite";

const ModulePage = observer(({module}) => {
  return (
    <Box component="main" width={1} padding={4} gap={4} display="flex" flexDirection="column">
      <ModuleHeader title={module.title}/>
      <UnitsGrid tokens={[]}/>
    </Box>
  )
});

export default ModulePage