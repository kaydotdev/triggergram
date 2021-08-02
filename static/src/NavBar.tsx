import { useState, MouseEvent } from 'react';
import {
    Typography,
    AppBar,
    Toolbar,
    IconButton,
    Button,
    MenuItem,
    Menu
} from '@material-ui/core';
import MenuIcon from '@material-ui/icons/Menu';
import {
    createStyles,
    makeStyles,
    Theme
} from '@material-ui/core/styles';
import './NavBar.css';


const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: { flexGrow: 1 },
        menuButton: { marginRight: theme.spacing(2) },
        title: { flexGrow: 1 }
    }),
);

function NavBar() {
    const classes = useStyles();
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

    const handleClick = (event: MouseEvent<HTMLButtonElement>) => { setAnchorEl(event.currentTarget); };
    const handleClose = () => { setAnchorEl(null); };

    return (
    <div className={classes.root}>
        <AppBar position="static">
            <Toolbar>
                <IconButton
                    edge="start"
                    className={classes.menuButton}
                    color="inherit"
                    aria-label="menu"
                >
                <MenuIcon />
                </IconButton>
                <Typography
                    variant="h6"
                    className={classes.title}
                >
                    Album on Functions
                </Typography>
                <Button
                    aria-controls="profile-menu"
                    aria-haspopup="true"
                    onClick={handleClick}
                >
                    Profile
                </Button>
                <Menu
                    id="profile-menu"
                    anchorEl={anchorEl}
                    keepMounted
                    open={Boolean(anchorEl)}
                    onClose={handleClose}
                >
                    <MenuItem onClick={handleClose}>Profile</MenuItem>
                    <MenuItem onClick={handleClose}>My account</MenuItem>
                    <MenuItem onClick={handleClose}>Logout</MenuItem>
                </Menu>
            </Toolbar>
        </AppBar>
    </div>
    );
}

export default NavBar;
