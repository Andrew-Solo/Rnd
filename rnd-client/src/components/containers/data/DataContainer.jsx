import {Box} from "@mui/material";
import DataHeader from "./DataHeader";

export default function DataContainer({children, ...props}) {
  return (
    <Box component="main" width={1} padding={4} gap={4} display="flex" flexDirection="column">
      <DataHeader {...props}/>
      {children}
    </Box>
  );
}