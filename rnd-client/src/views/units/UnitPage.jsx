import UnitHeader from "./UnitHeader";
import {Box} from "@mui/material";
import {observer} from "mobx-react-lite";
import InstancesGrid from "../instances/grid/InstancesGrid";
import PageLoader from "../ui/PageLoader";

const UnitPage = observer(({unit}) => {
  const instances = unit.instances;
  const {loaded, data} = instances;

  if (!loaded) return (<PageLoader/>);

  return (
    <Box component="main" width={1} padding={4} gap={4} display="flex" flexDirection="column">
      <UnitHeader title={unit.title}/>
      <InstancesGrid instances={data}/>
    </Box>
  )
});

export default UnitPage