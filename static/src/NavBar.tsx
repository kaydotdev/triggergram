import { useState, MouseEvent } from 'react';
import {
    AppBar,
    Toolbar,
    IconButton,
    Button,
    MenuItem,
    Menu
} from '@material-ui/core'
import {
    Menu as MenuIcon,
    MoreVert as MoreVertIcon
} from '@material-ui/icons';
import {
    createStyles,
    makeStyles,
    Theme
} from '@material-ui/core/styles';
import './NavBar.css';
import AppBanner from './banner.svg'


const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: { flexGrow: 1 },
        menuButton: { marginRight: theme.spacing(2) },
        banner: {
            flexGrow: 1,
            maxWidth: '150px',
            marginTop: '5px'
        },
        profileButton: {
            right: 0,
            position: 'absolute'
        }
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
                <img src={AppBanner} className={classes.banner} alt="TRIGGERGRAM" />
                <Button
                    className={classes.profileButton}
                    aria-controls="profile-menu"
                    aria-haspopup="true"
                    onClick={handleClick}
                >
                    <MoreVertIcon />
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
