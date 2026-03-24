use sha2::{Sha256, Digest};
use std::slice;

// Struktura przesyłana do C# (Musi być repr(C))
#[repr(C)]
pub struct Vertex {
    pub x: f32,
    pub y: f32,
    pub z: f32,
}

#[no_mangle]
pub extern "C" fn check_core_status() {
    // Ta funkcja służy do testu połączenia
    println!("[NV-CORE] RUST ENGINE ONLINE");
}

#[no_mangle]
pub extern "C" fn get_cube_edges(x: f32, y: f32, z: f32, vertices: *mut Vertex) {
    let v = unsafe { slice::from_raw_parts_mut(vertices, 8) };
    
    // Generujemy 8 punktów narożnych kostki 3D
    v[0] = Vertex { x, y, z };
    v[1] = Vertex { x: x + 1.0, y, z };
    v[2] = Vertex { x: x + 1.0, y: y + 1.0, z };
    v[3] = Vertex { x, y: y + 1.0, z };
    v[4] = Vertex { x, y, z: z + 1.0 };
    v[5] = Vertex { x: x + 1.0, y, z: z + 1.0 };
    v[6] = Vertex { x: x + 1.0, y: y + 1.0, z: z + 1.0 };
    v[7] = Vertex { x, y: y + 1.0, z: z + 1.0 };
}

#[no_mangle]
pub extern "C" fn hash_player_password(password: *const u8, len: usize, output: *mut u8) {
    let pass_slice = unsafe { slice::from_raw_parts(password, len) };
    let mut hasher = Sha256::new();
    hasher.update(pass_slice);
    let result = hasher.finalize();
    
    unsafe {
        std::ptr::copy_nonoverlapping(result.as_ptr(), output, 32);
    }
}