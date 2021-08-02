import { CssBaseline } from '@material-ui/core';
import {
  createTheme,
  ThemeProvider,
  makeStyles,
  Theme,
  createStyles
} from '@material-ui/core/styles';
import { Fab } from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add';
import { indigo, blue } from '@material-ui/core/colors';
import './App.css';
import NavBar from './NavBar'


const theme = createTheme({
  palette: {
    type: 'light',
    primary: indigo,
    secondary: blue
  }
});


const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    absolute: {
      position: 'absolute',
      bottom: theme.spacing(3),
      right: theme.spacing(3),
    },
  }),
);


function App() {
  const classes = useStyles();

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline/>
      <NavBar />
      <Fab color="secondary" className={classes.absolute}>
        <AddIcon />
      </Fab>
    </ThemeProvider>
  );
}

export default App;
