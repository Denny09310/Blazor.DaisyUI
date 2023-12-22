const attributeName = "data-theme"
const storageKey = "theme"

// Select the node that will be observed for mutations
const targetNode = document.querySelector("html");

// Options for the observer (which mutations to observe)
const config = { attributes: true };

// Callback function to execute when mutations are observed
const callback = () => {
    if (!targetNode.getAttribute(attributeName)) {
        const theme = localStorage.getItem(storageKey);
        if (theme) {
            targetNode.setAttribute(attributeName, theme)
        }
    }
};

// Create an observer instance linked to the callback function
const observer = new MutationObserver(callback);

// Start observing the target node for configured mutations
observer.observe(targetNode, config);

// Later, you can stop observing
observer.disconnect();

callback();