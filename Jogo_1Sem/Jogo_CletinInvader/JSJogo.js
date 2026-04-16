//Conexão com o canvas
const scoreEl = document.querySelector('#scoreEl')
const canvas = document.querySelector('canvas')
const c = canvas.getContext('2d')




//Determina que o tamanho do canvas seja igual o da tela
canvas.width = 1024
canvas.height = 576

//Criando o Cleitinho
class Player{
    //Construção dele vai ser dentro dessa classe
    constructor() {


        //Propriedades de velocidade
        this.velocity = {
            x:0,
            y:0
        }

        //faz o Cleitin virar quando pressionar as teclas de direita e esquerda
        this.rotation = 0
        this.opacity = 1

        //Construindo e posicionando (qual imagem, altura, largura posicionamento)
        const image = new Image()
        image.src = './img/wizard.png'
        image.onload = () => { //todas as propriedades abaixo funcionam se a imagem carregar
            const scale = 0.15
            this.image = image
            this.width = image.width * scale
            this.height = image.height * scale
            this.position = {
                x: canvas.width / 2 - this.width / 2,
                y: canvas.height - this.height - 20
            }
        }
    }

    //Pega tudo que fizemos acima e desenha o Cletin
    draw(){
        //c.fillStyle = 'red'
        //c.fillRect(this.position.x, this.position.y,this.width,this.height)

        c.save()
        c.globalAlpha = this.opacity
        c.translate(
            player.position.x + player.width/2,
            player.position.y + player.height/2
        )

        c.rotate(this.rotation)
        c.translate(
            -player.position.x - player.width/2,
            -player.position.y - player.height/2
        )


            if (this.image) {
                c.drawImage(this.image,
                    this.position.x,
                    this.position.y,
                    this.width,
                    this.height)
            }
            c.restore()

    }

    //Faz o Cleitin andar pros dois lados e atirar a cada frame
    update(){
        if (this.image) {
            this.draw()
            this.position.x += this.velocity.x
        } 
    }
}

//Criando os feitiços do Cleitinho
class Projectile{
    constructor({position, velocity}) {
        this.position = position
        this.velocity = velocity

        this.radius = 4
    }

    draw(){
        c.beginPath()
        c.arc(this.position.x, this.position.y, this.radius, 0, Math.PI *2)
        c.fillStyle = 'purple'
        c.fill()
        c.closePath()
    }

    update(){
        this.draw()
        this.position.x += this.velocity.x
        this.position.y += this.velocity.y
    }
}

class Particle{
    constructor({position, velocity, radius, color, fades}) {
        this.position = position
        this.velocity = velocity

        this.radius = radius
        this.color = color
        this.opacity = 1
        this.fades = fades
    }

    draw(){
        c.save()
        c.globalAlpha = this.opacity
        c.beginPath()
        c.arc(this.position.x, this.position.y, this.radius, 0, Math.PI *2)
        c.fillStyle = this.color
        c.fill()
        c.closePath()
        c.restore()
    }

    update(){
        this.draw()
        this.position.x += this.velocity.x
        this.position.y += this.velocity.y
        
        if (this.fades) this.opacity -= 0.01
    }
}


//Cria a bala do monstro
class InvaderProjectile{
    constructor({position, velocity}) {
        this.position = position
        this.velocity = velocity

        this.width = 3
        this.height = 10
    }

    draw(){
        c.fillStyle = 'green'
        c.fillRect(this.position.x, this.position.y, this.width,
            this.height)
    }

    update(){
        this.draw()
        this.position.x += this.velocity.x
        this.position.y += this.velocity.y
    }
}


//Cria o monstrinho
class Invader{
    //Construção dele vai ser dentro dessa método
    constructor({position}) {
        //Propriedades de velocidade
        this.velocity = {
            x:0,
            y:0
        }

        //Construindo e posicionando (qual imagem, altura, largura posicionamento)
        const image = new Image()
        image.src = './img/invader.png'
        image.onload = () => { //todas as propriedades abaixo funcionam se a imagem carregar
            const scale = 0.06
            this.image = image
            this.width = image.width * scale
            this.height = image.height * scale
            this.position = {
                x: position.x,
                y: position.y
            }
        }
    }

    //Pega tudo que fizemos acima e desenha o Monstro
    draw(){
        //c.fillStyle = 'red'
        //c.fillRect(this.position.x, this.position.y,this.width,this.height)

        if (this.image) {
            c.drawImage(this.image,
                this.position.x,
                this.position.y,
                this.width,
                this.height)
        }
    }

    //Faz o Monstro andar pros dois lados e atirar a cada frame
    update({velocity}){
        if (this.image) {
            this.draw()
            this.position.x += velocity.x
            this.position.y += velocity.y
        }
    }

    shoot(invaderProjectiles){
        invaderProjectiles.push(new InvaderProjectile({
            position:{
                x: this.position.x + this.width / 2,
                y: this.position.y + this.height
            },
            velocity:{
                x: 0,
                y: 5
            }
        })
        )
    }
}

//Criação de vários monstros atacando o Cleitin
class Grid{
    constructor() {
        this.position = {
            x: 0,
            y: 0
        }
        
        this.velocity = {
            x: 3,
            y: 0
        }
        this.invaders = []
        const columns = Math.floor(Math.random() * 10 + 5) //Gera n° de colunas aletórias entre 2 e 7 (quando for 5 ele soma com o dois, quando for 0 ele soma com o 2 pra não ter nada vazio)
        const rows = Math.floor(Math.random() * 5 + 2) //Gera n° de linhas aletórias entre 2 e 7 (quando for 5 ele soma com o dois, quando for 0 ele soma com o 2 pra não ter nada vazio)

        this.width = columns * 30

        for (let x = 0; x < columns; x++) {
            for (let y = 0; y < rows; y++) {
                this.invaders.push(new Invader({position:{
                    x: x * 30,
                    y: y * 30
                }
            })
            )
        }
        }

    }

    update (){
        this.position.x += this.velocity.x
        this.position.y += this.velocity.y

        this.velocity.y = 0

        if (this.position.x + this.width >= canvas.width
            || this.position.x <= 0){
            this.velocity.x = -this.velocity.x
            this.velocity.y = 30
        }
    }
}


//Faz o Cleitin aparecer na tela
const player = new Player();
const projectiles = []
const grids = []
const invaderProjectiles = []
const particles = []


const keys = {
    a: {
        pressed:false
    },
    d: {
        pressed:false
    },
    space: {
        pressed:false
    }
}

let frames = 0
let randomInterval = Math.floor(Math.random() * 500 + 500)
let game = {
    over: false,
    active: true
}

let score = 0

for (let i = 0; i<100; i++){
    particles.push(new Particle({
        position:{
            x: Math.random() * canvas.width,
            y: Math.random() * canvas.height
        },
        velocity:{
            x:0,
            y:0.3
        },
        radius: Math.random()*3,
        color: 'white'
        //color: '#7CB47E'
    }))
}

function createParticles({object,color,fades}) {
    for (let i = 0; i<15; i++){
        particles.push(new Particle({
            position:{
                x: object.position.x + object.width / 2,
                y: object.position.y + object.height / 2
            },
            velocity:{
                x:(Math.random() - 0.5)*2,
                y:(Math.random() - 0.5)*2
            },
            radius: Math.random()*3,
            color: color || '#7CB47E',
            fades
            //color: '#7CB47E'
        }))
    }
}

//Cria um loop de animação
function animate(){
    if (!game.active) return
    requestAnimationFrame(animate)
    c.fillStyle = 'black'
    c.fillRect(0,0,canvas.width,canvas.height)
    player.update()
    
    particles.forEach((particle, i) => {
        if (particle.position.y - particle.radius >= canvas.height){
            particle.position.x = Math.random() * canvas.width
            particle.position.y = -particle.radius
        }

        if (particle.opacity <= 0){
            setTimeout(() => {
                particles.splice(i,1)
            },0)
        }else{
            particle.update()
        }
    })
    
    invaderProjectiles.forEach((invaderProjectile, index) =>{
        if (invaderProjectile.position.y + invaderProjectile.height >=
        canvas.height){
            setTimeout(() => {
                invaderProjectiles.splice(index,1)
            },0)
        }else invaderProjectile.update()

        //projectile hits player
        if (invaderProjectile.position.y + invaderProjectile.height
            >= player.position.y && invaderProjectile.position.x +
            invaderProjectile.width >= player.position.x &&
            invaderProjectile.position.x <= player.position.x +
            player.width){

            setTimeout(() => {
                invaderProjectiles.splice(index,1)
                player.opacity = 0
                game.over = true
            },0)

            setTimeout(() => {
                game.active = false
                window.alert("Você perdeu 😭!\nRecarregue a página e jogue novamente!")
            },2000)

            createParticles({
                object: player,
                color: 'white',
                fades: true
            })
        }
    })

    projectiles.forEach((projectiles, index) => {
        if (projectiles.position.y + projectiles.radius <= 0){
            setTimeout(() => {
                projectiles.splice(index,1)
            },0)
        } else{
            projectiles.update()
        }
        projectiles.update()
    })

    grids.forEach((grid, gridIndex) => {
        grid.update()
        //spawn projectiles
        if (frames % 100 === 0 && grid.invaders.length > 0){
            grid.invaders[Math.floor(Math.random() * grid.invaders.
                length)].shoot(invaderProjectiles)
        }
        
        grid.invaders.forEach((invader, i) => {
            invader.update({velocity:grid.velocity })

            //projectiles hit enemy
            projectiles.forEach((projectile, j) => {
                if (projectile.position.y - projectile.radius <=
                    invader.position.y + invader.height &&
                    projectile.position.x + projectile.radius >=
                    invader.position.x && projectile.position.x -
                    projectile.radius <= invader.position.x + invader.width
                    && projectile.position.y + projectile.radius >=
                    invader.position.y){
                    
                    setTimeout(()=> {
                        const invaderFound = grid.invaders.find(
                            (invader2) => invader2 === invader
                            )
                        const projectileFound = projectiles.find(
                            (projectile2) => projectile2 === projectile
                        )

                        //remove invader and projectile
                        if (invaderFound && projectileFound){
                            score += 100
                            scoreEl.innerHTML = score
                            createParticles({
                                object: invader,
                                fades: true
                            })

                            grid.invaders.splice(i,1)
                            projectiles.splice(j,1)

                            if(grid.invaders.length > 0){
                                const firstInvader = grid.invaders[0]
                                const lastInvader = grid.invaders[grid.
                                    invaders.length -1]

                                grid.width = lastInvader.position.x -
                                    firstInvader.position.x + lastInvader.width

                                grid.position.x = firstInvader.position.x
                            }else{
                                grids.splice(gridIndex,1)
                            }
                        }
                    },0)
                }
            })
        })
    })
    
    if (keys.a.pressed && player.position.x >= 0){ //pressionado a ele vai pra esquerda e vai até o limite do canvas
        player.velocity.x = -7
        player.rotation = -.15
    }
    else if (keys.d.pressed && player.position.x + player.width <= canvas.width){// pressionado a ele vai pra direita e faz essa operação pra delimitar o canvas na direita (left side of our player + right side of our player <= right side of screen)
        player.velocity.x = 7
        player.rotation = .15
    }
    else{
        player.velocity.x = 0
        player.rotation = 0
    }

    //spawning enemies
    if (frames % randomInterval === 0){
        grids.push(new Grid())
        randomInterval = Math.floor(Math.random() * 500 + 500)
        frames = 0
        
    }
    frames++
}
animate()

//Faz o Cleitinho se mover através dos controles do teclado
addEventListener('keydown', ({key}) => {
    if (game.over) return

    switch (key){
        case 'a':
            keys.a.pressed = true
            break
        case 'd':
            keys.d.pressed = true
            break
        case ' ':
            projectiles.push(new Projectile({
                    position: {
                        x: player.position.x + player.width / 1.25,
                        y: player.position.y
                    },
                    velocity:{
                        x:0,
                        y:-10
                    }
            }))
            break
    }
})

addEventListener('keyup', ({key}) => {
    switch (key){
        case 'a':
            keys.a.pressed = false
            break
        case 'd':
            keys.d.pressed = false
            break
        case ' ':
            break
    }
})