export const beforeWebStart = () => {
    injectScript(document.head, "theme-observer.js")
}

function injectScript(targetNode, name) {
    const script = document.createElement('script');
    script.setAttribute('src', "./_content/Blazor.DaisyUI/" + name)
    targetNode.appendChild(script)
}