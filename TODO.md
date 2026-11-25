# Todo list for BindOpen

## Next tasks

### ⚙️ Configuration, Hosting & Logging
- [ ] **Configuration Integration**: Read configuration from `appsettings.json` (`SetConfiguration` extension).
- [ ] **Object Resolution in Settings**: Support reading objects directly in settings (e.g., `/$kernel`).
- [ ] **Logging Integration**: Deal with loggers (`ILogger`), evaluate if a startup logger makes sense, and ensure `SetLogger` is guessed automatically.

### 🚀 Performance & AOT Readiness
- [ ] **Switch to Manual Extension Registration**: Replace expensive assembly scanning (`AddAssemblyFrom`) with explicit registration to eliminate startup latency and ensure Native AOT compatibility.
  - *Example:*
    ```csharp
    scope.ExtensionStore.Add(BdoData.NewTaskDefinition<TaskFake>("tests.task"));
    ```
- [ ] **Implement Creation Delegates**: Avoid `Activator.CreateInstance` by providing direct instantiation logic.
  - *Example:*
    ```csharp
    .WithCreationDelegate(() => new TaskFake())
    ```
- [ ] **Develop Source Generators**: Automate the mapping logic between `BdoMetaObject` and POCOs to remove runtime Reflection (`GetValue`/`SetValue`).

### 🛡️ Scripting & Robustness
- [ ] **Static Script Validation**: Create a utility to pre-compile and validate script syntax in configuration files before runtime.
  - *Example:* Running `interpreter.Evaluate(script)` against a mock context during unit tests.
- [ ] **Standardize Serialization Syntax**: Ensure consistency between `$this` (keyword) and function calls to avoid "Magic String" mismatches in `ToString()` output.
- [ ] **Strongly-Typed Script Functions**: Map function names to constants instead of hardcoded strings.
  - *Example:* Use `public const string Name = "testEqual";` in your function classes.

### 🏗️ Architecture, Metadata & DX (Developer Experience)
- [ ] **Fluent Encapsulation Wrappers**: Hide BindOpen complexity behind simple service interfaces.
  - *Example:*
    ```csharp
    public void RunFakeTask(bool val) => _scope.Execute(new TaskFake { BoolValue = val });
    ```
- [ ] **Manual Mapping Interface**: Implement a "Fast-Path" mapping interface to bypass the automatic Reflection-based mapper.
  - *Example:* Adding a `Map(IBdoMetaObject meta)` method to your Entities for direct assignment.
- [ ] **Schema-Driven Documentation**: Use `BdoSchema` to enforce data contracts and generate human-readable error logs automatically.
- [ ] **Nullable Meta Schema by Default**: Make `MetaSchema` null by default and update validation so `MetaSet` items can still be inspected when the schema is null.
- [ ] **Fluent Config Builders (DX Alternatives)**:
  - **Tuple dictionary approach**:
    ```csharp
    var config = NewConfig("test-config",
        ("comment", "Sunny day"),
        ("temperature", new[] { 25, 26, 26 }), // Array = Multi-scalar
        ("date", DateTime.Now),
        NewNode("subscriber",
            ("name", "Ernest E."),
            ("code", 1560)
        )
    );
    ```
  - **Anonymous object approach**:
    ```csharp
    var config = NewConfig("test-config", new {
        comment = "Sunny day",
        temperature = new[] { 25, 26, 26 },
        date = DateTime.Now,
        subscriber = NewNode(new {
            name = "Ernest E.",
            code = 1560
        })
    });
    ```
  - **String-driven approach**:
    ```csharp
    var config = NewConfig("test-config")
        .With("comment:Sunny day", "temperature:25,26,26")
        .With("date:" + DateTime.Now)
        .WithNode("subscriber", "name:Ernest E.", "code:1560");
    ```

### 🧪 Advanced Testing
- [ ] **Recursive Tree Validation**: Add regression tests for deep hierarchical structures (5+ levels) in `TreeFake`.
- [ ] **Runtime Mode Simulation**: Validate that tasks behave correctly in `RuntimeModes.Simulation`, ensuring logs are populated without data side effects.

---

## Roadmap / Stuff to check out later on

### v1.3 — Dynamic Loading
- [ ] Loading connectors, entities, functions, and tasks dynamically.

### v1.2 — .NET Core Ecosystem
- [ ] Full integration with .NET Core logging and configuration abstractions.

### v1.1 — Core Refactoring & API Polish
- [ ] **Hosting & Setup**:
  - Instantiate host with `AddBindOpen` method.
  - Add `SetConfiguration` extension.
  - Make logging work again and auto-guess `SetLogger`.
- [ ] **Scripting & Data Types**:
  - Refactor `ScriptingFactory` -> `BdoScript`.
  - Simplify script interpretation.
  - Add `Expression` data value type.
  - Optimize serialization and fix XSD file support.
- [ ] **Extensions & Modules**:
  - Improve extension and extension item handling.
  - Improve `OptionsSet`.
  - Improve `AppModule` management (`DbFluent` mode).
- [ ] **API & Error Handling**:
  - Handle exceptions correctly across scopes.
  - `ToApiDto`: Use standard `Result` class.
- [ ] **Documentation**:
  - Implement a relevant `README.md` file (including *Getting Help* and *Contributors* sections).